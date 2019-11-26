import { Component, OnInit } from '@angular/core';
import { UsersService } from './../../services/users/users.service';
import { Router } from '@angular/router';
import { User } from './../../interfaces/users/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userName: string;
  password: string;
  constructor(private usersService: UsersService, private router: Router)
  { }

  ngOnInit() {
  }

  loginUser(){
    
    const user= <User> {
      UserName: this.userName,
      Password: this.password
    }
    
    this.usersService.loginUser(user)
      .subscribe(result => {
        debugger;
        console.log(result);
        if (result.StatusCode == 200)
          this.router.navigate(['activities']);
      });
  }

  getUsers(){
    console.log(this.userName);
    this.usersService.getUsers()
      .subscribe(result => {
        console.log(result);
        //this.router.navigate(["/activities"]);
      });
  }
}
