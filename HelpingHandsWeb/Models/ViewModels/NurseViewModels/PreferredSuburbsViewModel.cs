using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.NurseViewModels
{
    public class ChooseSuburbsViewModel
    {
        public int NurseID { get; set; }

        public List<SuburbViewModel> Suburbs { get; set; }
    }

    public class SuburbViewModel
    {
        public int SuburbId { get; set; }
        public string Suburb { get; set; }
    }

}
