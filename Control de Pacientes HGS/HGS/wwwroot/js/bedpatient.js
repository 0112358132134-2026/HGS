const BedChanged = (URL) => {

    $.post(URL, {

        "bedId": $("#BedId option:selected").val()

    }, function (data) {

        document.getElementById("Bed_Size").value = data.bed.size;
        document.getElementById("Bed_Annotations").value = data.bed.annotations;
        document.getElementById("Bed_AreaSucursalName").value = data.bed.areaSucursalName;
    });
};

const PatientChanged = (URL) => {

    $.post(URL, {

        "patientId": $("#PatientId option:selected").val()

    }, function (data) {

        document.getElementById("Patient_Dpi").value = data.patient.dpi;        
        document.getElementById("Patient_Name").value = data.patient.name; 
        document.getElementById("Patient_Lastname").value = data.patient.lastname; 
        document.getElementById("Patient_Birthdate").value = data.patient.birthdate; 
        document.getElementById("Patient_Observations").value = data.patient.observations; 
    });
};

const DoctorChanged = (URL) => {

    $.post(URL, {

        "doctorId": $("#DoctorId option:selected").val()

    }, function (data) {

        document.getElementById("Doctor_CollegiateNumber").value = data.doctor.collegiateNumber;
        document.getElementById("Doctor_Dpi").value = data.doctor.dpi;
        document.getElementById("Doctor_Name").value = data.doctor.name;
        document.getElementById("Doctor_Lastname").value = data.doctor.lastname;
        document.getElementById("Doctor_Birthdate").value = data.doctor.birthdate;
        document.getElementById("Doctor_SpecialtyName").value = data.doctor.specialtyName;        
    });
};