﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Framework.Dependency
{
    /// <summary>
    /// This class is used to pass configuration/options while registering classes in conventional way.
    /// </summary>
    public class ConventionalRegistrationConfig : DictionaryBasedConfig
    {
        /// <summary>
        /// Install all <see cref="IInterceptor"/> implementations automatically or not.
        /// Default: true. 
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// Creates a new <see cref="ConventionalRegistrationConfig"/> object.
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}
