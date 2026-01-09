using MediatR;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos.ProfileStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.ProfileStats.Queries;

public class GetScoreBoardQuery :RequestParameters, IRequest<PagedList<ScoreBoardDto>>
{

}
