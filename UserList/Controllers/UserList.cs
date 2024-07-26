using Microsoft.AspNetCore.Mvc;
using UserList.Models;
using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using static UserList.Models.showUserList;

namespace UserList.Controllers
{
    public class UserList : Controller
    {
        private IConfiguration _configuration;

        public UserList(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult UserLists()
        {
            return View();
        }
        public IActionResult ShowUserLists()
        {
            showUserList userlists = new showUserList(_configuration);
            ViewBag.table = userlists.showdata();
            return View();


        }
        public IActionResult Editdata(UserListData user,int userid) {
            int id = userid;
            string firstname = user.firstName;
            string lastname = user.lastName;
            int salary = user.salary;
            string gender = user.gender;
            showUserList userlists = new showUserList(_configuration);
            Console.WriteLine(userid);
            userlists.EditData(userid, firstname, lastname, salary, gender);
            ViewBag.table = userlists.showdata();
            return RedirectToAction("ShowUserLists", "UserList");
        }
        [HttpPost]
        public IActionResult addUser(UserListData user )
        {
            string firstname = user.firstName;
            string lastname= user.lastName;
            int salary=user.salary;
            string gender=user.gender;
            showUserList userlists = new showUserList(_configuration);
            userlists.AddData( firstname, lastname, salary, gender);
            ViewBag.table = userlists.showdata();
            return RedirectToAction("ShowUserLists", "UserList");
        }
        public IActionResult DeleteData(int id)
        {
            showUserList userlists = new showUserList(_configuration);
            userlists.DeleteData( id);
            ViewBag.table = userlists.showdata();
            return RedirectToAction("ShowUserLists", "UserList");
        }
        public IActionResult CreateUser(int userid=0, string firstName="", string lastName = "", int salary = 0, string gender = "")
        {
            ViewBag.action = "addUser";
            if (userid != 0)
            {
                ViewBag.action = "Editdata";
            }
            ViewBag.userid = userid;
            
            return View();
        }
    }
}


