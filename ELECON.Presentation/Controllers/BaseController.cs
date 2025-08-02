using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ELECON.Presentation.Controllers;

public class BaseController(IMediator mediator):Controller
{
    private readonly IMediator _mediator=mediator;


    public async Task<string?> ValidateModel<T>(IValidator<T> validator, T model)
    {
        ValidationResult validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
            return (await validator.ValidateAsync(model)).Errors[0].ErrorMessage;
        return null;

    }
    
}