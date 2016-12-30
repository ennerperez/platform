using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Platform.Support.Data.Attributes
{
    /// <summary>Proporciona un atributo de uso general que permite especificar las cadenas traducibles de los tipos y miembros de las clases parciales de entidad.</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class DisplayAttribute : Attribute
    {
        //private Type _resourceType;

        private string _shortName;

        private string _name;

        private string _description;

        private string _prompt;

        private string _groupName;

        private bool? _autoGenerateField;

        private bool? _autoGenerateFilter;

        private int? _order;

        /// <summary>Obtiene o establece un valor que se usa para la etiqueta de columna de la cuadrícula.</summary>
        /// <returns>Un valor para la etiqueta de columna de la cuadrícula.</returns>
        public string ShortName
        {
            get
            {
                return this._shortName;
            }
            set
            {
                if (this._shortName != value)
                {
                    this._shortName = value;
                }
            }
        }

        /// <summary>Obtiene o establece un valor que se usa para mostrarlo en la interfaz de usuario.</summary>
        /// <returns>Un valor que se usa para mostrarlo en la interfaz de usuario.</returns>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                }
            }
        }

        /// <summary>Obtiene o establece un valor que se usa para mostrar una descripción en la interfaz de usuario.</summary>
        /// <returns>Valor que se usa para mostrar una descripción en la interfaz de usuario.</returns>
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                if (this._description != value)
                {
                    this._description = value;
                }
            }
        }

        /// <summary>Obtiene o establece un valor que se usará para establecer la marca de agua para los avisos en la interfaz de usuario.</summary>
        /// <returns>Un valor que se usará para mostrar una marca de agua en la interfaz de usuario.</returns>
        public string Prompt
        {
            get
            {
                return this._prompt;
            }
            set
            {
                if (this._prompt != value)
                {
                    this._prompt = value;
                }
            }
        }

        /// <summary>Obtiene o establece un valor que se usa para agrupar campos en la interfaz de usuario.</summary>
        /// <returns>Valor que se usa para agrupar campos en la interfaz de usuario.</returns>
        public string GroupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                if (this._groupName != value)
                {
                    this._groupName = value;
                }
            }
        }

        ///// <summary>Obtiene o establece el tipo que contiene los recursos para las propiedades <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> y <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" />.</summary>
        ///// <returns>Tipo del recurso que contiene las propiedades <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" />, <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> y <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" />.</returns>
        //public Type ResourceType
        //{
        //    get
        //    {
        //        return this._resourceType;
        //    }
        //    set
        //    {
        //        if (this._resourceType != value)
        //        {
        //            this._resourceType = value;
        //            this._shortName.ResourceType = value;
        //            this._name.ResourceType = value;
        //            this._description.ResourceType = value;
        //            this._prompt.ResourceType = value;
        //            this._groupName.ResourceType = value;
        //        }
        //    }
        //}

        /// <summary>Obtiene o establece un valor que indica si la interfaz de usuario se debe generar automáticamente para mostrar este campo.</summary>
        /// <returns>true si la interfaz de usuario se debe generar automáticamente para mostrar este campo; de lo contrario, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">Se intentó obtener el valor de propiedad antes de establecerse.</exception>
        public bool AutoGenerateField
        {
            get
            {
                if (!this._autoGenerateField.HasValue)
                {
                    throw new InvalidOperationException(string.Format("Property {0} not set in {1}", "AutoGenerateField", "GetAutoGenerateField"));
                }
                return this._autoGenerateField.Value;
            }
            set
            {
                this._autoGenerateField = new bool?(value);
            }
        }

        /// <summary>Obtiene o establece un valor que indica si la interfaz de usuario se debe generar automáticamente para mostrar el filtrado de este campo. </summary>
        /// <returns>true si la interfaz de usuario se debe generar automáticamente para mostrar el filtrado de este campo; de lo contrario, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">Se intentó obtener el valor de propiedad antes de establecerse.</exception>
        public bool AutoGenerateFilter
        {
            get
            {
                if (!this._autoGenerateFilter.HasValue)
                {
                    throw new InvalidOperationException(string.Format("Property {0} not set in {1}", "AutoGenerateFilter", "GetAutoGenerateFilter"));
                }
                return this._autoGenerateFilter.Value;
            }
            set
            {
                this._autoGenerateFilter = new bool?(value);
            }
        }

        /// <summary>Obtiene o establece el peso del orden de la columna.</summary>
        /// <returns>Peso del orden de la columna.</returns>
        public int Order
        {
            get
            {
                if (!this._order.HasValue)
                {
                    throw new InvalidOperationException(string.Format("Property {0} not set in {1}", "Order", "GetOrder"));
                }
                return this._order.Value;
            }
            set
            {
                this._order = new int?(value);
            }
        }

        /// <summary>Inicializa una nueva instancia de la clase <see cref="T:System.ComponentModel.DataAnnotations.DisplayAttribute" />.</summary>
        public DisplayAttribute()
        {
        }

        /// <summary>Devuelve el valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" />.</summary>
        /// <returns>Cadena traducida para la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" /> si se ha especificado la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" /> representa una clave de recurso; de lo contrario, el valor no traducido de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ShortName" />.</returns>
        public string GetShortName()
        {
            return /*this._shortName.GetLocalizableValue() ??*/ this.GetName();
        }

        /// <summary>Devuelve un valor que se usa para mostrar campos en la interfaz de usuario.</summary>
        /// <returns>Cadena traducida para la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> si se ha especificado la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> representa una clave de recurso; de lo contrario, el valor no traducido de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">Se han inicializado las propiedades <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" />, pero no se pudo encontrar una propiedad estática pública con un nombre que coincida con el valor <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Name" /> de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" />.</exception>
        public string GetName()
        {
            return this._name;
        }

        /// <summary>Devuelve el valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" />.</summary>
        /// <returns>Descripción traducida si se ha especificado <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> representa una clave de recurso; de lo contrario, el valor no traducido de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" />.</returns>
        /// <exception cref="T:System.InvalidOperationException">Se han inicializado las propiedades <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" />, pero no se pudo encontrar una propiedad estática pública con un nombre que coincida con el valor <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Description" /> de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" />.</exception>
        public string GetDescription()
        {
            return this._description;
        }

        /// <summary>Devuelve el valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" />.</summary>
        /// <returns>Obtiene la cadena traducida para la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> si se ha especificado la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" /> representa una clave de recurso; de lo contrario, el valor no traducido de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Prompt" />.</returns>
        public string GetPrompt()
        {
            return this._prompt;
        }

        /// <summary>Devuelve el valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.GroupName" />.</summary>
        /// <returns>Un valor que se usará para agrupar los campos en la interfaz de usuario, si se ha inicializado <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.GroupName" />; de lo contrario, null.Si se ha especificado la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.ResourceType" /> y la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.GroupName" /> representa una clave de recurso, se devuelve una cadena traducida; de lo contrario, se devuelve una cadena no traducida.</returns>
        public string GetGroupName()
        {
            return this._groupName;
        }

        /// <summary>Devuelve el valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField" />.</summary>
        /// <returns>Valor de <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField" /> si se ha inicializado la propiedad; de lo contrario, es null.</returns>
        public bool? GetAutoGenerateField()
        {
            return this._autoGenerateField;
        }

        /// <summary>Devuelve un valor que indica si la interfaz de usuario se debe generar automáticamente para mostrar el filtrado de este campo. </summary>
        /// <returns>Valor de <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateFilter" /> si se ha inicializado la propiedad; de lo contrario, es null.</returns>
        public bool? GetAutoGenerateFilter()
        {
            return this._autoGenerateFilter;
        }

        /// <summary>Devuelve el valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Order" />.</summary>
        /// <returns>Valor de la propiedad <see cref="P:System.ComponentModel.DataAnnotations.DisplayAttribute.Order" /> si se ha establecido; de lo contrario, es null.</returns>
        public int? GetOrder()
        {
            return this._order;
        }
    }
}
