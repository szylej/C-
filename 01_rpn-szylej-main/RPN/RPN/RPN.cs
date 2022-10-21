using System;
using System.Collections.Generic;

namespace RPNCalulator {
	public class RPN {

		private Stack<int> _operators;

		public int EvalRPN(string input) {
	
			_operators = new Stack<int>();
			IsOperation isopeartion = new IsOperation();
            IsNumberCheck isnumbercheck = new IsNumberCheck();
            IsOperatorCheck isoperatorcheck = new IsOperatorCheck();

            var splitInput = input.Split(' ');
			foreach (var op in splitInput)
			{
                if (isnumbercheck.IsNumber(op))
					_operators.Push(Int32.Parse(op));
				else
                if (isoperatorcheck.IsOperator(op))
				{
					var num1 = _operators.Pop();
					var num2 = _operators.Pop();
					//_operators.Push(_operationFunction[op](num1, num2));
					_operators.Push(isopeartion.Operation(op)(num1, num2));
				}
			}

			var result = _operators.Pop();
			if (_operators.IsEmpty)
			{
				return result;
			}
			throw new InvalidOperationException();
		}

	}

	public class IsNumberCheck
	{
		private int BinToDec(String input)
		{
			string InputWithRemovedPrefix = input.Remove(0, 1);
            int dec = Convert.ToInt32(input, 2);

			return dec;
        }

		private int HexToDec(String input)
		{
            string InputWithRemovedPrefix = input.Remove(0, 1);
            int hex = Convert.ToInt32(input, 16);

            return hex;
        }

		public Func<string, int> IsNumber(String input) =>
			(s) =>
			(
				(input[0] == "D" ? s.Remove(0, 1) :
					(input[0] == "B") ? BinToDec(input) :
						(input[0] == "#" ? HexToDec(input) : 
			);
    }

	public class IsOperatorCheck
	{
        public bool IsOperator(String input) =>
            input.Equals("+") || input.Equals("-") ||
            input.Equals("*") || input.Equals("/");
    }

	public class IsOperation
	{
        public Func<int, int, int> Operation(String input) =>
            (x, y) =>
            (
				(input.Equals("+") ? x + y :
                    (input.Equals("*") ? x * y :
                        (input.Equals("-") ? x - y :
                            (input.Equals("/") ? x / y : int.MinValue))))
            );
    }
}