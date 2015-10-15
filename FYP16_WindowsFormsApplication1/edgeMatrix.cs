/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP16_WindowsFormsApplication1
{
        public static class Matrix
        {
            public static double[,] Laplacian3x3
            {
                get
                {
                    return new double[,]  
                { 
                  { -1, -1, -1,  }, 
                  { -1,  8, -1,  }, 
                  { -1, -1, -1,  }, 
                };
                }
            }
        }
    }

