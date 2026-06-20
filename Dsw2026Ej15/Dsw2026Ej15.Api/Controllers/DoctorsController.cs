using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Dsw2026Ej15.Data;
using Dsw2026Ej15.Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorRequest request)
        {
            if (request is null)
                throw new ValidationException("Body requerido.");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Name es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("LicenseNumber es requerido.");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
                throw new ValidationException("SpecialityId debe existir.");

            var doctor = new Doctor
            {
                Name = request.Name.Trim(),
                LicenseNumber = request.LicenseNumber.Trim(),
                IsActive = true,
                Speciality = speciality
            };

            _persistence.AddDoctor(doctor);

            var response = MapToResponse(doctor);
            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, response);
        }

        [HttpGet]
        public IActionResult GetAllActive()
        {
            var doctors = _persistence
                .GetDoctors()
                .Where(d => d.IsActive)
                .Select(MapToResponse)
                .ToList();

            return Ok(doctors);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor is null || !doctor.IsActive)
                throw new NotFoundException("No encontrado");

            var response = MapToResponse(doctor);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor is null || !doctor.IsActive)
                throw new NotFoundException("No encontrado");

            doctor.IsActive = false;
            _persistence.UpdateDoctor(doctor);

            return NoContent();
        }

        private static DoctorResponse MapToResponse(Doctor doctor)
        {
            return new DoctorResponse
            {
                Id = doctor.Id,
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            };
        }
    }
}
