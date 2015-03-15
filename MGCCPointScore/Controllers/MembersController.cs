﻿using MGCCPointScore.Models;
using Repositories.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MGCCPointScore.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private IMemberRepository memberRepo;

        public MembersController(IMemberRepository memberRepo)
        {
            this.memberRepo = memberRepo;
        }

        //
        // GET: /Members/
        public ActionResult MemberList()
        {
            IEnumerable<ClubMember> currentMembers = memberRepo.AllMembers;

            return View(currentMembers.Select(x => new ClubMemberViewModel { Name = x.Name, Id = x.Id }));
        }

        //
        // GET: /Members/
        public ActionResult Create()
        {
            return View(new ClubMemberViewModel());
        }

        //
        // POST: /Members/
        [HttpPost]
        public async Task<ActionResult> Create(ClubMemberViewModel model)
        {
            try
            {
                var member = new ClubMember(model.Name, Sex.Other, "");
                memberRepo.Add(member);
                return Redirect("MemberList");
            }
            catch (DuplicateMemberNameException)
            {
                ModelState.AddModelError("", string.Format("'{0}' already exists in the database.", model.Name));
                return View(model);
            }
        }

        //
        // GET: /Members/
        public ActionResult Edit(Guid? id)
        {
            if(id == null)
            {
                ModelState.AddModelError("", "Member with that Id does not exist...are you trying to hack things??");
                return Redirect("MemberList");
            }

            try
            {
                ClubMember member = memberRepo.GetFromId(id.Value);
                return View(new ClubMemberViewModel(member));
            }
            catch(MemberNotFoundException)
            {
                ModelState.AddModelError("", "Member with that Id does not exist...are you trying to hack things??");
                return Redirect("MemberList");
            }
        }

        //
        // POST: /Members/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(ClubMemberViewModel model)
        {
            try
            {
                var member = model.Retrieve();
                memberRepo.Update(member);
                return Redirect("MemberList");
            }
            catch (DuplicateMemberNameException)
            {
                ModelState.AddModelError("", string.Format("'{0}' already exists in the database.", model.Name));
                return View("Edit", model);
            }
        }
    }
}