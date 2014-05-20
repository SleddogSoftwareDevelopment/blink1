﻿using System;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1.Commands
{
    internal class SavePresetsCommand : IBlink1Command
    {
        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.InactivityMode,
                Convert.ToByte(0xBE),
                Convert.ToByte(0xEF),
                Convert.ToByte(0xCA),
                Convert.ToByte(0xFE),
                Convert.ToByte(0x00),
                Convert.ToByte(0x00)
            };
        }
    }
}