export class ClientBase {
    protected transformOptions(options: RequestInit) {
        options.headers = {
            ...options.headers,
        };
        return Promise.resolve(options);
    }
}