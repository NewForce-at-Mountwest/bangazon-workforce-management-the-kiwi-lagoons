using System.Collections.Generic;

namespace BangazonWorkforceMVC.Models.ViewModels
{
    public class TrainingProgramViewModel
    {
        public TrainingProgram TrainingProgram { get; set; }
        public List<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();

    }
}
