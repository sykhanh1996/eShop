import { Component, OnInit } from '@angular/core';
import { User } from '@app/shared/models';
import { UserService } from '@app/shared/services/users.services';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users$: Observable<User[]>;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.users$ = this.userService.getAll();
  }

}
