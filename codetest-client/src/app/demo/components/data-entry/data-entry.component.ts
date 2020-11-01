import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ScoreService } from '../../services/score.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ValidationErrorHandler } from '../../../shared/validation-error-handler';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { ScoreAdd } from '../../models/score-add';
import { Team } from '../../models/team';
import { Game } from '../../models/Game';
@Component({
  selector: 'app-data-entry',
  templateUrl: './data-entry.component.html',
  styleUrls: ['./data-entry.component.scss']
})
export class DataEntryComponent implements OnInit {

  // editorSettings;
  teams: Team[];
  games: Game[];
  postForm: FormGroup;

  constructor(
    private router: Router,
    private scoreService: ScoreService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
  ) { }

  ngOnInit() {
    this.getTeams();
    this.getGames();
    this.postForm = this.fb.group({
      gameTitle: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      employee: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      teamA: ['', [Validators.required]],
      teamB: ['', [Validators.required]],
      teamAscore: [0],
      teamBscore: [0]
    });

    // this.editorSettings = this.tinymce.getSettings();
  }

  // submit() {
  //   if (this.postForm.dirty && this.postForm.valid) {
  //     this.scoreService.addScore(this.postForm.value).subscribe(
  //       post => {
  //         this.router.navigate(['/demo/log-list']);
  //       },
  //       validationResult => {
  //         this.snackBar.open('There are validation errors!', 'Close', { duration: 3000 });
  //         ValidationErrorHandler.handleFormValidationErrors(this.postForm, validationResult);
  //       });
  //   }
  // }
  
  getTeams() {
    this.scoreService.getTeamList()
    .subscribe(resp => {
      this.teams = resp.body;
    });
  }
  getGames() {
    this.scoreService.getGameList()
    .subscribe(resp => {
      this.games = resp.body; 
    });
  }
  openConfirmDialog(post: ScoreAdd) {
    const confirm = {
      content: post.employee+' confirm to add : '+post.gameTitle,
      title:post.teamA+' vs '+post.teamB+' - '+post.teamAscore+' : '+post.teamBscore,
      confirmAction: 'Add',
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { dialog: confirm }
    });

    dialogRef
      .afterClosed()
      .subscribe(
        post => {
          if (post) {
            this.scoreService.addScore(this.postForm.value).subscribe(
              post => {
                this.router.navigate(['/demo/log-list']);
              },
              validationResult => {
                this.snackBar.open('There are validation errors!', 'Close', { duration: 3000 });
                ValidationErrorHandler.handleFormValidationErrors(this.postForm, validationResult);
              });
          }
        }
      );
  }

}
