//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Langulag.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class TagOfClient
    {
        public int ID { get; set; }
        public int IDTag { get; set; }
        public int IDClient { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
