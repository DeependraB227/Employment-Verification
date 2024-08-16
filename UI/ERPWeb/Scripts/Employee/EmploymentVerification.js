function GetSearch() { 
    var formData = new FormData();

    var EmpId = parseInt($('#intEmployeeID').val());
    var CompName = $("#txtCompanyName").val();
    var VCode = $("#txtVerificationCode").val();

    if (isNaN(EmpId)) {
        alert('Please Input Employee ID !!');
        return;
    }
    if (CompName == '') {
        alert('Company Name Required !!');
        return;
    }
    if (VCode == '') {
        alert('Verification Code Required !!');
        return;
    }

    formData.append("EmployeeID", EmpId);
    formData.append("CompanyName", CompName);
    formData.append("VerificationCode", VCode);

    ShowLoader();

    $.ajax({
        url: '/Employee/getEmploymentVerificationResult',
        processData: false,
        contentType: false,
        type: 'post',
        cache: false,
        data: formData,
        datatype: 'json',
        success: function (Result) {
            $("#divInner").remove();
            $("#divsearch").hide();
            $("txtResult").val(Result);
            //$("#createDiv").append(htmlData);
            $("#createDiv").show();
            HideLoader();
        }
    });

}