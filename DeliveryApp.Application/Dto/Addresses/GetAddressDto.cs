﻿namespace DeliveryApp.Application.Dto.Addresses;

public class GetAddressDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CountryId { get; set; }
    public string Country {  get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public int AddressTypeId { get; set; }
    public bool GuestAddress { get; set; }
}
