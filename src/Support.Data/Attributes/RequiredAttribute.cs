using System;
using System.ComponentModel.DataAnnotations;

namespace Platform.Support.Data.Attributes
{
    /// <summary>Especifica que un campo de datos necesita un valor.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredAttribute : ValidationAttribute
    {
        //      [CompilerGenerated]
        //      [Serializable]
        //      private sealed class _c
        //{
        //	public static readonly RequiredAttribute.<>c<>9 = new RequiredAttribute.<>c();

        //      public static Func<string> <>9__0_0;

        //	internal string ctor>b__0_0()
        //      {
        //          return DataAnnotationsResources.RequiredAttribute_ValidationError;
        //      }
        //}

        /// <summary>Obtiene o establece un valor que indica si se permite una cadena vacía.</summary>
        /// <returns>Es true si se permite una cadena vacía; de lo contrario, es false.El valor predeterminado es false.</returns>
        public bool AllowEmptyStrings
        {
            get;
            set;
        }

        /// <summary>Inicializa una nueva instancia de la clase <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" />.</summary>
        public RequiredAttribute() : base()
        {
            //         Func<string> arg_20_1;
            //         if ((arg_20_1 = RequiredAttribute.<> c.<> 9__0_0) == null)
            //{
            //             arg_20_1 = (RequiredAttribute.<> c.<> 9__0_0 = new Func<string>(RequiredAttribute.<> c.<> 9.<.ctor > b__0_0));
            //         }
            //         base..ctor(arg_20_1);
        }

        /// <summary>Comprueba si el valor del campo de datos necesario no está vacío.</summary>
        /// <returns>Es true si la validación se realiza correctamente; de lo contrario, es false.</returns>
        /// <param name="value">Valor del campo de datos que se va a validar.</param>
        /// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">El valor del campo de datos es null.</exception>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            string text = value as string;
            return text == null || this.AllowEmptyStrings || text.Trim().Length != 0;
        }
    }
}