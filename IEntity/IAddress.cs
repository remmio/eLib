using System;

namespace eLib.IEntity
{
  
    public interface IAddress
    {
         Guid AddressGuid { get; set; }       
         string Email { get; set; }      
         string PhoneNumber { get; set; }      
         string Line1 { get; set; }       
         string Line2 { get; set; }        
         string PostCode { get; set; }       
         string Town { get; set; }      
         string Country { get; set; }       
    }
}
