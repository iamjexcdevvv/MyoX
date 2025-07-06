import { Component, inject, OnInit } from "@angular/core";
import {
	FormGroup,
	FormControl,
	ReactiveFormsModule,
	Validators,
	AbstractControl,
} from "@angular/forms";
import { passwordMatchValidator } from "../../../shared/validators/password-match.validator";
import { AuthService } from "../../../core/auth/auth.service";
import { UserRegister } from "../../../shared/models/user-register.model";
import { AuthResult } from "../../../shared/models/auth-result.model";
import { getErrorMessage } from "../../../shared/utils/form-error.util";

@Component({
	selector: "register",
	templateUrl: "register.component.html",
	imports: [ReactiveFormsModule],
})
export class RegisterComponent implements OnInit {
	private readonly _authService = inject(AuthService);

	registerForm = new FormGroup(
		{
			email: new FormControl("", [Validators.required, Validators.email]),
			password: new FormControl("", [Validators.minLength(8)]),
			confirmPassword: new FormControl(""),
			userAgreeWithRegulations: new FormControl(false, [
				Validators.requiredTrue,
			]),
		},
		{ validators: passwordMatchValidator() }
	);

	constructor() {}

	ngOnInit() {}

	onSubmit() {
		if (this.registerForm.valid) {
			const payload: UserRegister = {
				email: this.registerForm.controls.email.value,
				password: this.registerForm.controls.password.value,
				confirmPassword:
					this.registerForm.controls.confirmPassword.value,
			};

			this._authService.register(payload).subscribe({
				next: (response: AuthResult) => {
					if (response.isSuccess) {
						alert("Your account has been succesfully registered");
					}
				},
				error: ({ error }) => {
					if (!error.isSuccess) {
						const code = error.error.code;
						const message = error.error.description;

						if (code === "UserAuthError.EmailAlreadyExists") {
							this.registerForm.get("email")?.setErrors({
								emailAlreadyExists: message,
							});
						}
					}
				},
			});
		}
	}

	getError(control: AbstractControl) {
		return getErrorMessage(control);
	}
}
