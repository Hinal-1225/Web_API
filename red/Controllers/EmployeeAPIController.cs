using red.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace red.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        DBEmployeeEntities context = new DBEmployeeEntities();
        public IHttpActionResult Get()
        {

            return Ok(context.TBLEmployees.ToList());
        }

        [Route("getdata/{id}")]
        public IHttpActionResult Get(int id)
        {
            TBLEmployee e = context.TBLEmployees.Where(a => a.ID == id).FirstOrDefault();


            return Ok(e);
        }
        [Route("insertdata")]
        public IHttpActionResult Post(TBLEmployee e)
        {
            try
            {
                if (e.Salary < 5000 || e.Salary > 100000)
                {
                    throw new SalaryException("Salary should not be less than 5000 and more than 100000");
                }

            }
            catch (SalaryException ex)
            {
                return InternalServerError(ex);
            }

            try
            {
                if (context.TBLEmployees.Where(a => a.Name == e.Name).SingleOrDefault() != null)
                {
                    //throw new NameException("Name Can Not be Same");
                    e.Name = e.Name + "1";
                }

            }
            catch (NameException ex)
            {
                return InternalServerError(ex);
            }

            context.TBLEmployees.Add(e);
            context.SaveChanges();
            return Ok();
        }

        
        public IHttpActionResult Delete(int id)
        {
            TBLEmployee e = context.TBLEmployees.Where(a => a.ID == id).FirstOrDefault();
            context.TBLEmployees.Remove(e);
            context.SaveChanges();
            return Ok();
        }
    }
}

public class SalaryException : Exception
{
    public SalaryException(String msg) : base(msg)
    {

    }

}
public class NameException : Exception
{
    public NameException(String msg) : base(msg)
    {

    }

}