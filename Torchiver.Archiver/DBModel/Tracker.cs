//------------------------------------------------------------------------------
// <auto-generated>
//    Tento kód byl generován ze šablony.
//
//    Ruční změny tohoto souboru mohou způsobit neočekávané chování v aplikaci.
//    Ruční změny tohoto souboru budou přepsány, pokud bude kód vygenerován znovu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Torchiver.Archiver.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tracker
    {
        public int TrackerId { get; set; }
        public string Url { get; set; }
        public int InfoInfoId { get; set; }
    
        public virtual Info Info { get; set; }
    }
}
