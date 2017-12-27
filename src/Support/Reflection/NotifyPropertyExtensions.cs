//using System.Collections.Generic;
//using System.ComponentModel;

//namespace Platform.Support.Reflection
//{
//    public static class NotifyPropertyExtensions
//    {
//        public static void SetField<U>(this object obj, ref U field, U value)
//        {
//#if (!PORTABLE)
//            if (!EqualityComparer<U>.Default.Equals(field, value))
//            {
//                var PropertyChanging = obj.GetType().GetEvent("PropertyChanging");
//                if (PropertyChanging != null)
//                {
//                    PropertyChanging.GetRaiseMethod(true).Invoke(obj, new object[] { new PropertyChangingEventArgs(field.ToString()) });
//                }
//#endif
//                field = value;
//                var PropertyChanged = obj.GetType().GetEvent("PropertyChanged");
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged.GetRaiseMethod(true).Invoke(obj, new object[] { new PropertyChangedEventArgs(field.ToString()) });
//                }
//#if (!PORTABLE)
//            }
//#endif
//        }

//        public static void SetField(this object obj, ref object field, object value)
//        {
//#if (!PORTABLE)
//            if (!EqualityComparer<object>.Default.Equals(field, value))
//            {
//                var PropertyChanging = obj.GetType().GetEvent("PropertyChanging");
//                if (PropertyChanging != null)
//                {
//                    PropertyChanging.GetRaiseMethod(true).Invoke(obj, new object[] { new PropertyChangingEventArgs(field.ToString()) });
//                }
//#endif
//                field = value;
//                var PropertyChanged = obj.GetType().GetEvent("PropertyChanged");
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged.GetRaiseMethod(true).Invoke(obj, new object[] { new PropertyChangedEventArgs(field.ToString()) });
//                }
//#if (!PORTABLE)
//            }
//#endif
//        }
//    }
//}