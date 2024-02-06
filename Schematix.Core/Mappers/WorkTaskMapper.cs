using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.Mappers;

public interface IWorkTaskMapper 
{ 
    IEnumerable<WorkTask> MapWorkTasksDto(IEnumerable<WorkTaskDto> workTaskDto);
    IEnumerable<WorkTaskDto> MapWorkTasks(IEnumerable<WorkTask> workTask);
    WorkTaskDto MapWorkTask(WorkTask workTask);
    WorkTask MapWorkTaskDto(WorkTaskDto workTaskDto);
}

public class WorkTaskMapper : IWorkTaskMapper
{
    public WorkTask MapWorkTaskDto(WorkTaskDto workTaskDto)
    {
        TaskType type;

        var newtype = Enum.TryParse(workTaskDto.Type, out type);

        if (Enum.TryParse(workTaskDto.Type, true, out type))
        {
            int intValue = (int)type;
        }

        return new WorkTask
        {
            Id = workTaskDto.Id,
            Start = workTaskDto.Start,
            End = workTaskDto.End,
            Type = type
        };
    }

    public WorkTaskDto MapWorkTask(WorkTask workTask)
    {
        
        return new WorkTaskDto
        {
            Id = workTask.Id,
            Start = workTask.Start,
            End = workTask.End,
            Type = workTask.Type.ToString(),
        };
    }

    public IEnumerable<WorkTaskDto> MapWorkTasks(IEnumerable<WorkTask> workTasks)
    {
        return workTasks.Select(MapWorkTask);
    }

    public IEnumerable<WorkTask> MapWorkTasksDto(IEnumerable<WorkTaskDto> workTaskDtos)
    {
        return workTaskDtos.Select(MapWorkTaskDto);
    }
}
