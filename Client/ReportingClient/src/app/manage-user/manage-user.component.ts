import { Component, OnInit } from '@angular/core';
import { ApplicationUser } from '../models/application-user';
import { switchMap, map } from 'rxjs/operators';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { LanguageService } from '../services/language.service';
import { UserService } from '../services/user.service';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../services/auth.service';
import { Subscription } from 'rxjs';
import { RoleService } from '../services/role.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.scss']
})
export class ManageUserComponent implements OnInit {
  user: ApplicationUser;
  companyId: string;
  userEmail: string;
  userForm: FormGroup;
  subscription: Subscription;
  updateSubscription: Subscription;
  roles: any[] = [];
  languages: any[] = [];

  constructor(
    private auth: AuthService,
    private titleService: Title,
    private userService: UserService,
    private languageService: LanguageService,
    private roleService: RoleService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  updateUser() {
    if (this.userForm.invalid)
      return;
    this.userForm.value.language = this.language.value.id;
    this.userForm.value.role = this.role.value.id;
    this.updateSubscription = this.userService.update(this.userForm.value).subscribe((user: ApplicationUser) => this.user = user);
  }

  cancel() {
    if (!confirm("Are you sure you want to cancel the changes?"))
      return;
    this.router.navigate(['/manage-users']);
  }

  ngOnInit() {
    this.titleService.setTitle("Respaunce | Manage user");
    this.userEmail = this.route.snapshot.paramMap.get('email');

    this.userForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      language: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });

    this.subscription = this.userService.getByEmail(this.userEmail)
      .pipe(
        switchMap((user: ApplicationUser) => {
          this.user = user;
          this.companyId = user.companyId;
          this.userForm.addControl("companyId", new FormControl(this.companyId, Validators.required));
          this.firstName.setValue(user.firstName);
          this.lastName.setValue(user.lastName);
          this.phoneNumber.setValue(user.phoneNumber);
          this.email.setValue(user.email);
          console.log("User: ", this.user);
          return this.languageService.getAll();
        }),
        switchMap(languages => {
          this.languages = languages;
          this.language.setValue(this.languages.find(language => language.name == this.user.language));
          return this.roleService.getAll();
        }),
        map(roles => {
          this.roles = roles;
          this.role.setValue(this.roles.find(role => role.name == this.user.role));
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    if (this.updateSubscription) this.updateSubscription.unsubscribe();
  }

  get firstName() {
    return this.userForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.userForm.get('lastName') as FormControl;
  }

  get phoneNumber() {
    return this.userForm.get('phoneNumber') as FormControl;
  }

  get email() {
    return this.userForm.get('email') as FormControl;
  }

  get language() {
    return this.userForm.get('language') as FormControl;
  }

  get role() {
    return this.userForm.get('role') as FormControl;
  }
}
