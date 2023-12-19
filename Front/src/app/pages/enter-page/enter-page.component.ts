import { Component, ViewChildren, QueryList } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../button/button.component';
import { InputComponent } from '../../input/input.component';
import { Router } from '@angular/router';
import { HeadComponent } from '../head/head.component';
import { EnterPageService } from './enter-page.service';
import { Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-enter-page',
  standalone: true,
  imports: [CommonModule, InputComponent, ButtonComponent, HeadComponent],
  templateUrl: './enter-page.component.html',
  styleUrl: './enter-page.component.css'
})
export class EnterPageComponent {

  @ViewChildren(InputComponent) children!: QueryList<InputComponent>;

  constructor(private router: Router, private authService: EnterPageService) {
   // this.router.navigate(['/worker']);
  }

  clickHandler(){
    const specificChild1 = this.children.toArray()[0];
    const specificChild = this.children.toArray()[1];
    this.authService.login(specificChild1.inputValue, specificChild.inputValue).subscribe(
      (response) => {
         console.log(response.status);
        if (response.status === 200) {
          const responseBody = response.body.position;
          this.authService.setSession(response.body);

          if(responseBody == "hradmin")
          {
            this.router.navigate(['/hradmin'],
            {
              queryParams:{
              login: specificChild1.Text
              }
        });
          }
          if(responseBody == "admin")
          {
            this.router.navigate(['/admin'],
            {
              queryParams:{
              login: specificChild1.Text
              }
        });
          }

          if(responseBody == "worker")
          {
            this.router.navigate(['/worker'],
            {
              queryParams:{
              login: specificChild1.Text
              }
        });
          }

          if(responseBody == "warehouseman")
          {
            this.router.navigate(['/warehouseman'],
            {
              queryParams:{
              login: specificChild1.Text
              }
        });
          }

        }
      },
      (error) => {
        console.error('Error during login:', error);
      }
    );
  }
}
