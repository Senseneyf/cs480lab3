/*
 * Author: Frank Senseney
 * Last modifed: November 1st 2017
 * 
 */

using System;
using System.Collections;
using System.Text.RegularExpressions;
namespace cs480lab3
{
    //enumerator list of all possible token types
    public enum TokenTypes
    {
        Unset,
        Number,
        Add,
        Subtract,
        Multiply,
        Power,
        Divide,
        LeftParenth,
        RightParthen,
        Negative
    }

    //token struct with fields for value and type
    public struct Token
    {
        public TokenTypes type;
        public string value;
    }

    class Rpn
    {
        public double Evaluate(string infix)
        {
            //Preform a series of regex replace operations on the string so that it can be easily tokenized
            //with numbers or operators, add a space to either side
            infix = Regex.Replace(infix, @"(?<number>\d+(\.\d+)?)", " ${number} ");
            infix = Regex.Replace(infix, @"(?<ops>[+\-*/^()])", " ${ops} ");
            //remove any two spaces in a row between characters, and remove any single spaces before or after a character
            infix = Regex.Replace(infix, @"\s+", " ").Trim();
            //regex replace operations to convert uniary minus to ~
            //convert all instances of minus sign to a placeholder 
            infix = Regex.Replace(infix, "-", "N");
            //if there are any minus signs with a number in front of it, return it to a normal minus sign
            infix = Regex.Replace(infix, @"(?<number>(\d+(\.\d+)?))\s+N", "${number} -");
            //replace placeholder with ~ 
            infix = Regex.Replace(infix, "N", "~");

            //start Shunting-yard algorithm for converting infix notation to postfix notation
            //tokenize input string into an array
            string[] parsedInput = infix.Split("".ToCharArray());

            Queue output = new Queue();
            Stack operators = new Stack();
            string postFix = "";

            Token cToken, opToken;
            for (int i = 0; i < parsedInput.Length; i++)
            {
                //create a new token
                cToken = new Token();
                cToken.value = parsedInput[i];

                //if the current token is a number, set the token type and add it to the queue
                if (Regex.IsMatch(parsedInput[i], @"^\d+$"))
                {
                    cToken.type = TokenTypes.Number;
                    output.Enqueue(cToken);
                }
                //addition operator
                if (parsedInput[i] == "+")
                {
                    //set the token type
                    cToken.type = TokenTypes.Add;
                    //if the stack is not empty
                    if (operators.Count > 0)
                    {
                        opToken = (Token)operators.Peek();
                        //while there is an operator at the top of the stack
                        while (IsOperator(opToken.type))
                        {
                            //pop the operator off the top of the stack and add it to the queue
                            output.Enqueue(operators.Pop());
                            //if theres another operator at the top of the stack, set it as the current opToken
                            if (operators.Count > 0)
                            {
                                opToken = (Token)operators.Peek();
                            }
                            else
                            {
                                //otherwise break out of the while loop
                                break;
                            }
                        }
                    }
                    //push the current token to the stack
                    operators.Push(cToken);
                }
                //subtraction case
                else if (parsedInput[i] == "-")
                {
                    //set the token type
                    cToken.type = TokenTypes.Subtract;
                    //if the stack is not empty
                    if (operators.Count > 0)
                    {
                        opToken = (Token)operators.Peek();
                        //while there is an operator at the top of the stack
                        while (IsOperator(opToken.type))
                        {
                            //pop the operator off the top of the stack and add it to the queue
                            output.Enqueue(operators.Pop());
                            //if theres another operator at the top of the stack, set it as the current opToken
                            if (operators.Count > 0)
                            {
                                opToken = (Token)operators.Peek();
                            }
                            else
                            {
                                //otherwise break out of the while loop
                                break;
                            }
                        }
                    }
                    //push the current token to the stack
                    operators.Push(cToken);
                }
                //multiplication case
                else if (parsedInput[i] == "*")
                {
                    //set the token type
                    cToken.type = TokenTypes.Multiply;
                    //if the stack is not empty
                    if (operators.Count > 0)
                    {
                        opToken = (Token)operators.Peek();
                        //while there is an operator at the top of the stack
                        while (IsOperator(opToken.type))
                        {
                            if (opToken.type == TokenTypes.Add || opToken.type == TokenTypes.Subtract)
                            {
                                break;
                            }
                            else
                            {
                                //pop the operator off the top of the stack and add it to the queue
                                output.Enqueue(operators.Pop());
                                //if theres another operator at the top of the stack, set it as the current opToken
                                if (operators.Count > 0)
                                {
                                    opToken = (Token)operators.Peek();
                                }
                                else
                                {
                                    //otherwise break out of the while loop
                                    break;
                                }
                            }
                        }
                    }
                    //push the current token to the stack
                    operators.Push(cToken);
                }
                //division operator
                else if (parsedInput[i] == "/")
                {
                    //set the token type
                    cToken.type = TokenTypes.Divide;
                    //if the stack is not empty
                    if (operators.Count > 0)
                    {
                        opToken = (Token)operators.Peek();
                        //while there is an operator at the top of the stack
                        while (IsOperator(opToken.type))
                        {
                            if (opToken.type == TokenTypes.Add || opToken.type == TokenTypes.Subtract)
                            {
                                break;
                            }
                            else
                            {
                                //pop the operator off the top of the stack and add it to the queue
                                output.Enqueue(operators.Pop());
                                //if theres another operator at the top of the stack, set it as the current opToken
                                if (operators.Count > 0)
                                {
                                    opToken = (Token)operators.Peek();
                                }
                                else
                                {
                                    //otherwise break out of the while loop
                                    break;
                                }
                            }
                        }
                    }
                    //push the current token to the stack
                    operators.Push(cToken);
                }
                //exponent case
                else if (parsedInput[i] == "^")
                {
                    //set the token type
                    cToken.type = TokenTypes.Power;
                    //push token onto the stack
                    operators.Push(cToken);
                }
                else if (parsedInput[i] == "(")
                {
                    //set the token type
                    cToken.type = TokenTypes.LeftParenth;
                    //push token onto the stack
                    operators.Push(cToken);
                }
                else if (parsedInput[i] == ")")
                {
                    //set the token type
                    cToken.type = TokenTypes.RightParthen;
                    if (operators.Count > 0)
                    {
                        opToken = (Token)operators.Peek();
                        //while the token at the top of the stack isn't a left parthenesis
                        while (opToken.type != TokenTypes.LeftParenth)
                        {
                            output.Enqueue(operators.Pop());
                            if (operators.Count > 0)
                            {
                                opToken = (Token)operators.Peek();
                            }
                            else
                            {
                                // If the stack empties without finding a left parenthese, they are mismatched
                                throw new Exception("Mismatched parenthesis");
                            }
                        }
                    }
                    //pop the current operator off the stack
                    operators.Pop();
                }
                //uninary minus operator
                else if (parsedInput[i] == "~")
                {
                    //set the token type
                    cToken.type = TokenTypes.Negative;
                    //push token onto the stack
                    operators.Push(cToken);
                }
            }

            //after all tokens have been read and there are still operators in the stack
            while (operators.Count > 0)
            {
                //pop the operator off the stack and copy it into opToken
                opToken = (Token)operators.Pop();
                //if the token taken off the stack is a left parenthesis, there is a missmatch
                if (opToken.type == TokenTypes.LeftParenth)
                {
                    throw new Exception("Mismatched parenthesis");
                }
                else
                {
                    //add the copy to the ouput queue
                    output.Enqueue(opToken);
                }
            }

            //for each token in the ouput queue
            foreach(Object t in output)
            {
                //copy each token in the output queue and append it to the postFix string
                opToken = (Token)t;
                //append token to the postFix expression result string, adding a space after each one
                postFix += opToken.value + " ";
            }
            //conversion to postfix complete
            Console.WriteLine(postFix);

            //begin evaluating the rpn expresison
            Stack answer = new Stack();
            Token aToken = new Token();
            double operator1 = 0.0;
            double operator2 = 0.0;
            foreach(Object o in output)
            {
                //copy the current token
                aToken = (Token)o;
                //if the token type is a number make it a double and push it to the stack
                if(aToken.type == TokenTypes.Number)
                {
                    answer.Push(double.Parse(aToken.value));
                }
                else if(aToken.type == TokenTypes.Add)
                {
                    if (answer.Count >= 2)
                    {
                        //pop operator 2 and 1 off the stack and add them together and push it to the stack
                        operator2 = (double)answer.Pop();
                        operator1 = (double)answer.Pop();
                        answer.Push(operator1 + operator2);
                    }
                    else
                    {
                        throw new Exception("Evaluation error 1");
                    }
                }
                else if (aToken.type == TokenTypes.Subtract)
                {
                    if (answer.Count >= 2)
                    {
                        //pop operator 2 and 1 off the stack and subtract op2 from op1 and push the result to the stack
                        operator2 = (double)answer.Pop();
                        operator1 = (double)answer.Pop();
                        answer.Push(operator1 - operator2);
                    }
                    else
                    {
                        throw new Exception("Evaluation error 2");
                    }
                }
                else if (aToken.type == TokenTypes.Divide)
                {
                    if (answer.Count >= 2)
                    {
                        //pop operator 2 and 1 off the stack and divide op1 by op2 and push the result to the stack
                        operator2 = (double)answer.Pop();
                        operator1 = (double)answer.Pop();
                        answer.Push(operator1 / operator2);
                    }
                    else
                    {
                        throw new Exception("Evaluation error 3");
                    }
                }
                else if (aToken.type == TokenTypes.Multiply)
                {
                    if (answer.Count >= 2)
                    {
                        //pop operator 2 and 1 off the stack and mutliply them together and push the result to the stack
                        operator2 = (double)answer.Pop();
                        operator1 = (double)answer.Pop();
                        answer.Push(operator1 * operator2);
                    }
                    else
                    {
                        throw new Exception("Evaluation error 4");
                    }
                }
                else if (aToken.type == TokenTypes.Power)
                {
                    if (answer.Count >= 2)
                    {
                        //pop operator 2 and 1 off the stack and use math.pow to
                        operator2 = (double)answer.Pop();
                        operator1 = (double)answer.Pop();
                        answer.Push(Math.Pow(operator1,operator2));
                    }
                    else
                    {
                        throw new Exception("Evaluation error 5");
                    }
                }
                else if (aToken.type == TokenTypes.Negative)
                {
                    if (answer.Count >= 1)
                    {
                        //pop operator1 off the stack and make it negative
                        operator1 = (double)answer.Pop();
                        answer.Push(-operator1);
                    }
                    else
                    {
                        throw new Exception("Evaluation error 6");
                    }
                }
            }
            //if there is only one token in the answser stack, return it
            if (answer.Count == 1)
            {
                return (double)answer.Pop();
            }
            else
            {
                throw new Exception("Evaluation error 7");
            }

        }

        //method to check token type
        private bool IsOperator(TokenTypes type)
        {
            //if the token type is a number or a parenthsis
            if (type == TokenTypes.Number || type == TokenTypes.LeftParenth || type == TokenTypes.RightParthen)
            {
                return false;
            }
            //otherwise its an operator
            else
            {
                return true;
            }
        }

    }

}
