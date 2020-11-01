import { QueryParameters } from '../../shared/query-parameters';

export class ScoreParameters extends QueryParameters {
    gameTitle?: string;

    constructor(init?: Partial<ScoreParameters>) {
        super(init);
        Object.assign(this, init);
    }
}
