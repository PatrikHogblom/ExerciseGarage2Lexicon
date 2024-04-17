using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExerciseGarage2Lexicon.Models;

namespace ExerciseGarage2Lexicon.Data
{
    public class ExerciseGarage2LexiconContext : DbContext
    {
        public ExerciseGarage2LexiconContext (DbContextOptions<ExerciseGarage2LexiconContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseGarage2Lexicon.Models.ParkedVehicle> ParkedVehicle { get; set; } = default!;
    }
}
