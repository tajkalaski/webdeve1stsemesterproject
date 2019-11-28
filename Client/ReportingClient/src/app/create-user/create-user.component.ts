import { LanguageService } from './../services/language.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApplicationUser } from '../models/application-user';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../services/auth.service';
import { Subscription } from 'rxjs';
import { UserService } from '../services/user.service';
import { RoleService } from '../services/role.service';
import { switchMap, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit, OnDestroy {
  createUserForm: FormGroup;
  subscription: Subscription;
  updateSubscription: Subscription;
  roles: any[] = [];
  languages: any[] = [];

  constructor(
    private titleService: Title,
    private auth: AuthService,
    private userService: UserService,
    private languageService: LanguageService,
    private roleService: RoleService,
    private router: Router
  ) { }

  createUser() {
    if (this.createUserForm.invalid) {
       return;
    }
    this.createUserForm.addControl('companyId', new FormControl(this.auth.user.companyId, Validators.required));
    this.createUserForm.addControl('companyName', new FormControl(this.auth.user.companyName, Validators.required));
    this.createUserForm.value.language = this.language.value.id;
    this.createUserForm.value.role = this.role.value.id;
    console.log(this.createUserForm.value);
    this.updateSubscription = this.userService.create(this.createUserForm.value)
      .subscribe((user: ApplicationUser) => {
        this.router.navigate(['/manage-users', user.email]);
      });
  }

  cancel() {
    if (!confirm('Are you sure you want to cancel the changes?')) {
      return;
    }
    this.router.navigate(['/manage-users']);
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Create user');

    this.createUserForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      language: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });

    this.subscription = this.languageService.getAll()
      .pipe(
        switchMap((languages: any[]) => {
          this.languages = languages;
          this.language.setValue(this.languages.find(language => language.id === '7cef445e-7fae-4514-b7fa-624982cfc130'));
          return this.roleService.getAll();
        }),
        map(roles => {
          this.roles = roles;
          this.role.setValue(this.roles.find(role => role.id === 'ef950206-c286-489e-8088-5ac34b95c1e7'));
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get firstName() {
    return this.createUserForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.createUserForm.get('lastName') as FormControl;
  }

  get phoneNumber() {
    return this.createUserForm.get('phoneNumber') as FormControl;
  }

  get email() {
    return this.createUserForm.get('email') as FormControl;
  }

  get language() {
    return this.createUserForm.get('language') as FormControl;
  }

  get role() {
    return this.createUserForm.get('role') as FormControl;
  }
}
