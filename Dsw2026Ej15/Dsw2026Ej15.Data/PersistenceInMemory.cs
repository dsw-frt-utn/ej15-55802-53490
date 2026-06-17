using Dsw2026Ej15.Dsw2026Ej15.Domain;
using System.Text.Json;

namespace Dsw2026Ej15.Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Speciality> _specialities = new();
        private readonly List<Doctor> _doctors = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "specialities.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"No se encontró el archivo requerido: {filePath}");

            var json = File.ReadAllText(filePath);

            var specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (specialities is not null)
                _specialities.AddRange(specialities);
        }

        public IReadOnlyCollection<Speciality> GetSpecialities()
            => _specialities;

        public Speciality? GetSpecialityById(Guid id)
            => _specialities.FirstOrDefault(s => s.Id == id);

        public IReadOnlyCollection<Doctor> GetDoctors()
            => _doctors;

        public Doctor? GetDoctorById(Guid id)
            => _doctors.FirstOrDefault(d => d.Id == id);

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            var index = _doctors.FindIndex(d => d.Id == doctor.Id);

            if (index >= 0)
                _doctors[index] = doctor;
        }
    }
}
