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
        Negative,
        Multiply,
        Power,
        Divide,
        LeftParenth,
        RightParthen
    }

    //token struct with its value and type
    public struct Token
    {
        public TokenTypes type;
        public string value;
    }

    class Rpn
    {
        public void ToPostfix(string infix)
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
            //if there are any minus signs with a number in front of it, its a normal minus sign
            infix = Regex.Replace(infix, @"(?<number>(\d+(\.\d+)?)))\s+N", "${number} -");
            //replace placeholder with ~ 
            infix = Regex.Replace(infix, "N", "~");


            //tokenize input string into an array
            string[] parsedInput = infix.Split("".ToCharArray());

            Queue output = new Queue();
            Stack operators = new Stack();
            string postFix = string.Empty;

            Token cToken, opToken;
            for (int i = 0; i < parsedInput.Length; i++)
            {
                //create a token
                cToken = new Token();
                cToken.value = parsedInput[i];

                //if the current token is a number, set the token type and add it to the queue
                if (Regex.IsMatch(parsedInput[i], @"^\d+$"))
                {
                    cToken.type = TokenTypes.Number;
                    output.Enqueue(cToken);
                }
                //addition case
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
                //division case
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
                else if(parsedInput[i] == "^")
                {
                    //set the token type
                    opToken.type = TokenTypes.Power;
                    //push token onto the stack
                    operators.Push(cToken);
                }
                else if (parsedInput[i] == "~")
                {
                    //set the token type
                    opToken.type = TokenTypes.Negative;
                    //push token onto the stack
                    operators.Push(cToken);
                }
                else if (parsedInput[i] == "(")
                {
                    //set the token type
                    opToken.type = TokenTypes.LeftParenth;
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
                        while (opToken.type != TokenTypes.LeftParenth)
                        {
                            output.Enqueue(operators.Pop());
                            if (operators.Count > 0)
                            {
                                opToken = (Token)operators.Peek();
                            }
                            else
                            {
                                // If the stack empties without finding a left parenthesis, they are mismatched
                                throw new Exception("Mismatched parenthesis");
                            }
                        }
                    }
                    //pop the current operator off the stack
                    operators.Pop();
                }
                else if(parsedInput[i] == "~")
                {
                    //set the token type
                    cToken.type = TokenTypes.LeftParenth;
                    //push token onto the stack
                    operators.Push(cToken);
                }
            }

            //after all tokens have been read and there are still operators in the stack
            while(operators.Count != 0)
            {
                //pop the operator off the stack and set opToken to it
                opToken = (Token)operators.Pop();
                //if the token taken off the stack is a left parenthesis, there is a missmatch
                if (opToken.type == TokenTypes.LeftParenth)
                {
                    throw new Exception("Mismatched parenthesis");
                }
                else
                {
                    output.Enqueue(opToken);
                }
            }

            //for each token in the ouput queue
            foreach(object o in output)
            {
                //copy each token in the output queue and append it to the postFix string
                opToken = (Token)o;
                postFix += string.Format("{0} ", opToken.value);
            }
            //Console.WriteLine(postFix);

        }

        //method to check if token type is an operator
        private bool IsOperator(TokenTypes t)
        {
            bool result = false;
            switch (t)
            {
                case TokenTypes.Add:
                case TokenTypes.Subtract:
                case TokenTypes.Multiply:
                case TokenTypes.Divide:
                case TokenTypes.Power:
                case TokenTypes.Negative:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

    }

}
