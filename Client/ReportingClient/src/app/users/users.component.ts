import { AccountService } from './../services/account.service';
import { UserService } from './../services/user.service';
import { ApplicationUser } from './../models/application-user';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, OnDestroy {
  users: ApplicationUser[] = [];
  subscription: Subscription;
  deleteSubscription: Subscription;

  constructor(
    private userService: UserService,
    private accountService: AccountService,
    private titleService: Title,
    private router: Router
  ) { }

  deleteUser(user: ApplicationUser) {
    if (!confirm('Are you sure you want to delete ' + user.email + '?')) { return; }
    if (user.role === 'Admin') { return alert('You cannot delete an admin user'); }
    this.deleteSubscription = this.userService.delete(user.email).subscribe(() => {
      const index = this.users.indexOf(user);
      this.users.splice(index);
    });
  }

  sortBy(key) {
    this.users.sort((n1, n2) => {
      if (n1[key] > n2[key]) { return 1; }
      if (n1[key] < n2[key]) { return -1; }
      return 0;
    });
  }

  ngOnInit() {
    this.titleService.setTitle('Respaunce | Manage users');
    this.subscription = this.userService.getByCompany()
      .pipe(
        map((users: ApplicationUser[]) => {
          this.users = users;
        })
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    if (this.deleteSubscription) { this.deleteSubscription.unsubscribe(); }
  }

}
