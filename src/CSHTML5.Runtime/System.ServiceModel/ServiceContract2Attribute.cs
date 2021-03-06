﻿
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  ● This code is dual-licensed (GPLv3 + Commercial). Commercial licenses can be obtained from: http://cshtml5.com
//
//  ● You are NOT allowed to:
//       – Use this code in a proprietary or closed-source project (unless you have obtained a commercial license)
//       – Mix this code with non-GPL-licensed code (such as MIT-licensed code), or distribute it under a different license
//       – Remove or modify this notice
//
//  ● Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product.
//
//===============================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ServiceModel
{
    /// <summary>
    /// Indicates that an interface or a class defines a service contract in a Windows
    /// Communication Foundation (WCF) application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ServiceContract2Attribute : Attribute
    {
         /// <summary>
        /// Initializes a new instance of the System.ServiceModel.ServiceContractAttribute
        /// class.
        /// </summary>
        public ServiceContract2Attribute()
        {
            Namespace = "http://tempuri.org/"; //default value
        }

        public ServiceContract2Attribute(Type CallbackContract = null, string ConfigurationName = null, string Name = null, string Namespace = null, string SOAPActionPrefix = null)
        {
            this.CallbackContract = CallbackContract;
            this.ConfigurationName = ConfigurationName;
            this.Name = Name;
            this.SOAPActionPrefix = SOAPActionPrefix;
            if (Namespace != null)
            {
                this.Namespace = Namespace;
            }
            else
            {
                this.Namespace = "http://tempuri.org/"; //default value
            }
        }

        /// <summary>
        /// Gets or sets the type of callback contract when the contract is a duplex
        /// contract.
        /// </summary>
        public Type CallbackContract { get; private set; }

        // Returns:
        //     The name used to locate the service element in an application configuration
        //     file. The default is the name of the service implementation class.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     The value is an empty string.
        /// <summary>
        /// The name used to locate the service element in an application configuration
        /// file. The default is the name of the service implementation class.
        /// </summary>
        public string ConfigurationName { get; private set; } //todo: set default value
        
        // Returns:
        //     The default value is the name of the class or interface to which the System.ServiceModel.ServiceContractAttribute
        //     is applied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The value is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     The value is an empty string.
        /// <summary>
        /// Gets or sets the name for the portType element in Web Services Description
        /// Language (WSDL).
        /// </summary>
        public string Name { get; private set; } //todo: set default value

        /// <summary>
        /// The string we use to determine the SOAPAction name by concatenating this string with the method name.
        /// </summary>
        public string SOAPActionPrefix { get; private set; }

        /// <summary>
        /// Gets or sets the namespace of the portType element in Web Services Description
        /// Language (WSDL).
        /// </summary>
        public string Namespace { get; private set; }
    }
}
