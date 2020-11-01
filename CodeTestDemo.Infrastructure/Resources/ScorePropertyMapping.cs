using System;
using System.Collections.Generic;
using System.Text;
using CodeTestDemo.Core.Entities;
using CodeTestDemo.Infrastructure.Services;

namespace CodeTestDemo.Infrastructure.Resources
{
    public class ScorePropertyMapping : PropertyMapping<ScoreResource, Score>
    {
        public ScorePropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>
                (StringComparer.OrdinalIgnoreCase)
            {
                [nameof(ScoreResource.GameTitle)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Score.GameTitle), Revert = false}
                    },
                [nameof(ScoreResource.TeamA)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Score.TeamA), Revert = false}
                    },
                [nameof(ScoreResource.Employee)] = new List<MappedProperty>
                    {
                        new MappedProperty{ Name = nameof(Score.Employee), Revert = false}
                    }
            })
        {
        }
    }
}
