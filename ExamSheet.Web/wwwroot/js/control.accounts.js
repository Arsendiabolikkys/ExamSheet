var $accountType = $("select.account-type");
var $referenceId = $("select.reference-id");

if ($accountType.length && $referenceId.length) {
    $(function () {
        function updateReferenceList() {
            var selected = $accountType.find("option:selected").text();
            if ($accountType.val() > 0) {
                $referenceId.find("optgroup:not([label='" + selected + "'])").hide();
                $referenceId.find("optgroup[label='" + selected + "']").show();
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