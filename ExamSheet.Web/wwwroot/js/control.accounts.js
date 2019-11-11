var $accountType = $("select.account-type");
var $referenceId = $("select.reference-id");

if ($accountType.length && $referenceId.length) {
    $(function () {
        function updateReferenceList() {
            var selected = $accountType.find("option:selected").text();
            if (selected) {
                var group = selected == "Викладач" ? "Teacher" : "Deanery";
                $referenceId.find("optgroup:not([label='" + group + "'])").hide();
                $referenceId.find("optgroup[label='" + group + "']").show();
                if ($referenceId.find("option:selected").not(":visible")) {
                    $referenceId.val("");
                }
            }
            else {
                $referenceId.find("optgroup").show();
            }
        }

        $("select.account-type").on("change", function (e) {
            updateReferenceList();
        });
    });
}

//TODO: Apply fix
//var $sR = $("select#ddlRegioni");
//var $sP = $("select#ddlProvince");
//var $sPclone = $sP.clone();

//function updateProvinceList() {
//    var regName = $sR.find("option:selected").text();
//    if ($sR.val() > 0) {
//        $sP.find("optgroup").remove();
//        $sP.append($sPclone.find("optgroup[label='" + regName + "']").clone());
//    }
//    else {
//        $sP.find("optgroup").remove();
//        $sP.append($sPclone.find("optgroup").clone());
//    }
//}

//$("select#ddlRegioni").on("change", function (e) {
//    updateProvinceList();
//});