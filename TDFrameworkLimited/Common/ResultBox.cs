// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : Güzel Program
// ================================

using System;
using System.Collections.Generic;
using System.Data;

namespace TDFramework.Common
{
    public sealed class ResultBox
    {
        public bool Result { get; set; }
        public dynamic Data { get; set; }
        public string ErrorMessage { get; set; }

        public ResultBox()
		{
            this.Result = false;
            this.ErrorMessage = null;
            this.Data = null;
        }
	}
}
