﻿using System.Runtime.Serialization;

namespace IPChecker.Exceptions
{
    public class IP2CCallException : Exception
    {
        public IP2CCallException() { }

        public IP2CCallException(string? message) : base(message) { }

    }
}
