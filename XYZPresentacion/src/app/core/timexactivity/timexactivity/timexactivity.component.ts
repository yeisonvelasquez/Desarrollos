import { Component, OnInit } from '@angular/core';
import { TimexactivityService } from './../../../services/timexactivity/timexactivity.service';
import { Timexactivity } from './../../../interfaces/timexactivity/timexactivity';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-timexactivity',
  templateUrl: './timexactivity.component.html',
  styleUrls: ['./timexactivity.component.css']
})
export class TimexactivityComponent implements OnInit {
  activityid: number;
  timeWorked: string;
  dateActivity: string;
  public lsttimexactivity: any;

  constructor(private timexactivityService: TimexactivityService, private activatedRoute: ActivatedRoute, private router: Router)
  { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      debugger;
    //   this.sub = this.activatedRoute
    //   .queryParams
    //   .subscribe(params => {
    //     // Defaults to 0 if no query param provided.
    //     this.page = +params['page'] || 0;
    //   });
      this.activityid = params['activityid'];
      console.log(this.activityid);

      this.timexactivityService.GetTimeXActivity(this.activityid)
        .subscribe(result => {
          debugger;
          console.log(result);
          if (result.StatusCode == 200 && result.Data != null){
              this.lsttimexactivity = result.Data;
            // this.lstActivities = result.Data;
            // this.router.navigate(['activities']);
          }
          else{
            alert(result.Message);
            if (result.StatusCode == 401)
              this.router.navigate(['login']);
          }
        });
    });
  }

  public registerTimeXActivity(){
    const timexactivity= <Timexactivity> {
      TimeWorked: this.timeWorked,
      DateActivity: this.dateActivity,
      IdActivity: this.activityid
    };
    
    this.timexactivityService.RegisterTimeXActivity(timexactivity)
      .subscribe(result => {
        debugger;
        console.log(result);
        if (result.StatusCode == 200){
          this.timeWorked = '';
          this.dateActivity = '';
          this.lsttimexactivity = result.Data;
          // this.router.navigate(['activities']);
        }
        else
        {
          alert(result.Message);
          if (result.StatusCode == 401)
            this.router.navigate(['login']);
        }
      });
  }

}
