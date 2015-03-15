namespace MGCCPointScore.Models
{
    using System.ComponentModel.DataAnnotations;
    using Repositories.Members;
    using System;
    using System.Web.Mvc;

    // TODO: Add in Sex and Note fields
    public class ClubMemberViewModel
    {
        public ClubMemberViewModel()
        {
            Id = Guid.Empty;
        }

        public ClubMemberViewModel(ClubMember member)
        {
            Name = member.Name;
            Id = member.Id;
        }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        

        public ClubMember Retrieve()
        {
            return new ClubMember(Name, Sex.Other, "") { Id = Id };
        }
    }
}
