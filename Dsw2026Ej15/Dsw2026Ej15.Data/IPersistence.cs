using Dsw2026Ej15.Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        IReadOnlyCollection<Speciality> GetSpecialities();
        Speciality? GetSpecialityById(Guid id);

        IReadOnlyCollection<Doctor> GetDoctors();
        Doctor? GetDoctorById(Guid id);

        void AddDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
    }
}
