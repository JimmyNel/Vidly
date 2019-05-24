using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using VidlyExercice1.Dto;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace VidlyExercice1.ViewModels
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check the selected Membership Type
            // ObjectInstance gives access to the containing class - in that case = Customer which implement the validation attribute
            var customer = (CustomerDto)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthdate == DateTime.MinValue)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - customer.Birthdate.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}