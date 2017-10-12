﻿namespace Morpher.WebService.V3
{
    public class InvalidFlagsException : InvalidArgumentException
    {
        private static readonly string ErrorMessage =
            "Указаны неправильные флаги.";

        public InvalidFlagsException()
            : base(ErrorMessage)
        {
        }
    }
}