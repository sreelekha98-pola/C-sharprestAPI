using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;



namespace SAMPLEAPI.Controllers
{
  


    public class EmployeeController : ApiController
    {
        empdbDataContext db = new empdbDataContext();

        public IEnumerable<Employee> Get()
        {
            //returning all records of table tblMember.  
            return db.Employees.ToList().AsEnumerable();
        }

        public HttpResponseMessage Get(int id)
        {

            var memberdetail = (from a in db.Employees where a.ID == id select a).FirstOrDefault();


            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID NOT FOUND");
            }
        }
                 
               
            
            
        public HttpResponseMessage Post(Employee _member)
        {
            try
            {
         
                db.Employees.InsertOnSubmit(_member);

               
                db.SubmitChanges();

               
                var msg = Request.CreateResponse(HttpStatusCode.Created, _member);
 
                msg.Headers.Location = new Uri(Request.RequestUri + _member.ID.ToString());

                return msg;
            }
            catch (Exception ex)
            {

                //return response as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, Employee _member)
        {

            var memberdetail = (from a in db.Employees where a.ID == id select a).FirstOrDefault();

            if (memberdetail != null)
            {
            
                memberdetail.FirstName = _member.FirstName;
                memberdetail.LastName = _member.LastName;
                memberdetail.Gender = _member.Gender;
                memberdetail.Salary = _member.Salary;
                db.SubmitChanges();


                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or details  Not Found");
            }
        }

        public HttpResponseMessage Delete(int id)
        {

            try
            {

                var _DeleteMember = (from a in db.Employees where a.ID == id select a).FirstOrDefault();

                if (_DeleteMember != null)
                {

                    db.Employees.DeleteOnSubmit(_DeleteMember);
                    db.SubmitChanges();


                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member Not Found or Invalid " + id.ToString());
                }
            }
            catch (Exception ex)
            {


                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

  }
 }
