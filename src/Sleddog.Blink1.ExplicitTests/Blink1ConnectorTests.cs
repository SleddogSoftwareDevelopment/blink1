﻿using System;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1ConnectorTests
    {
        [RequireBlinkHardware]
        public void CanIdentify()
        {
            Assert.DoesNotThrow(() => Blink1Connector.Identify(TimeSpan.FromSeconds(1)));
        }

        [RequireBlinkHardware]
        public void ScanFindsDevices()
        {
            var devices = Blink1Connector.Scan();

            Assert.NotEmpty(devices);
        }

        [RequireNoBlinkHardware]
        public void ScanWithNoDevicesFindsNone()
        {
            var devices = Blink1Connector.Scan();

            Assert.Empty(devices);
        }
    }
}