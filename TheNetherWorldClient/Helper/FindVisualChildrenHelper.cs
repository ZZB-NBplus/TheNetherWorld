using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace logisticsms.DAL.Helper
{
    public static class FindVisualChildrenHelper
    {
        public static List<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            List<T> children = new List<T>();
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        children.Add((T)child);
                    }

                    List<T> childOfChild = FindVisualChildren<T>(child);
                    if (childOfChild != null && childOfChild.Count > 0)
                    {
                        children.AddRange(childOfChild);
                    }
                }
            }
            return children;
        }
    }
}
