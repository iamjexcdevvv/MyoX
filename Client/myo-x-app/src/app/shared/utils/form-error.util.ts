import { AbstractControl } from "@angular/forms";

export function getErrorMessage(control: AbstractControl): string | null {
	if (!control || !control.errors) return null;

	if (control.hasError("required")) return "This field is required";
	if (control.hasError("email")) return "Invalid email format";
	if (control.hasError("minlength"))
		return "Password should consist at least 8 characters";
	if (control.hasError("emailAlreadyExists"))
		return control.getError("emailAlreadyExists");
	if (control.hasError("passwordMismatch")) return "Passwords don’t match";

	return null;
}
