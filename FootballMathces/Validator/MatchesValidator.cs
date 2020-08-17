using FluentValidation;
using FootballMathces.Models;
using System;

namespace FootballMatches.Validator
{
    public class MatchesValidator  : AbstractValidator<Match> {

        public MatchesValidator()
        {
            RuleFor(x => x.Place).NotNull();
            RuleFor(x => x.HostPlayers).Must(list1 => list1.Count > 5).WithMessage("At least six playest must be checked");
            RuleFor(m => m.GuestPlayers).Must(list2 => list2.Count > 5).WithMessage("At least six playest must be checked");
            DateTime now = DateTime.Now;
            RuleFor(m => m.GuestId).NotEqual(m => m.HostId).WithMessage("Cant be two same teams");
            RuleFor(m => m.HostId).NotEqual(m => m.GuestId).WithMessage("Cant be two same teams"); ;
            RuleFor(m => m.Time).NotEmpty().GreaterThan(now).WithMessage("Date can't be in past");
        }
    }
}
