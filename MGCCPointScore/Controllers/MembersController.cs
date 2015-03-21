using MGCCPointScore.Models;
using Repositories.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            //return View(currentMembers.Select(x => new ClubMemberViewModel { Name = x.Name, Id = x.Id }));
            return View(currentMembers.Select(x => new ClubMemberViewModel(x)));
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
                //var member = new ClubMember(model.Name, Sex.Other, "");
                memberRepo.Add(model.Retrieve());
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

        // GET: /Members/Delete/GUID
        public ActionResult Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ClubMember member = memberRepo.GetFromId(id.Value);
                return View(new ClubMemberViewModel(member));
            }
            catch (MemberNotFoundException)
            {
                return HttpNotFound();
            }
        }

        // POST: /Members/Delete/GUID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                ClubMember member = memberRepo.GetFromId(id);
                memberRepo.Delete(member);   
            }
            catch (MemberNotFoundException)
            {
                // TODO: Maybe log here??
            }
            return RedirectToAction("MemberList", "Members");
        }
    }
}
