﻿using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT_LAB_WEB_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}