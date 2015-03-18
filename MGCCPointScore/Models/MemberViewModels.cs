namespace MGCCPointScore.Models
{
    using System.ComponentModel.DataAnnotations;
    using Repositories.Members;
    using System;
    using System.Web.Mvc;

    public enum ViewModelSex
    {
        Male,
        Female,
        Other
    }

    // TODO: Add in Sex and Note fields
    public class ClubMemberViewModel
    {
        public ClubMemberViewModel()
        {
            Id = Guid.Empty;
        }

        public ClubMemberViewModel(ClubMember member)
        {
            Id = member.Id;
            Name = member.Name;
            VMSex = Convert(member.Sex);
            Note = member.Note;
        }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ViewModelSex VMSex { get; set; }
        public string Note { get; set; }    

        public ClubMember Retrieve()
        {
            return new ClubMember(Name, Convert(VMSex), Note) { Id = Id };
        }

        private ViewModelSex Convert(Sex sex)
        {
            switch(sex)
            {
                case Sex.Female:
                    return ViewModelSex.Female;
                case Sex.Male:
                    return ViewModelSex.Male;
                default:
                    return ViewModelSex.Other;
            }
        }

        private Sex Convert(ViewModelSex sex)
        {
            switch(sex)
            {
                case ViewModelSex.Female:
                    return Sex.Female;
                case ViewModelSex.Male:
                    return Sex.Male;
                default:
                    return Sex.Other;
            }
        }
    }
}
