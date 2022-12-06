import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserLogin } from '../_models/userLogin';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  user: UserLogin = {username: 'giang123', password: '123'};
  constructor(public accountsService: AccountsService) { }

  ngOnInit(): void {
  }
  
  
  login(){
    this.accountsService.login(this.user).subscribe(
      (response) => {
        console.log(response);
      }
    );
  }

}
