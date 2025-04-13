Updates 
1. No need to create greeter_pb.d.ts


2. File greeter_pb.js export the message as 
				goog.exportSymbol('proto.helloworld.HelloReply', null, global);
				goog.exportSymbol('proto.helloworld.HelloRequest', null, global);
				goog.exportSymbol('proto.helloworld.HelloRequest2', null, global);
 
 3. FIle greeter_grpc_web_pb.js export the greeter_pb.js as follows:
 			const proto = {};
			proto.helloworld = require('./greeter_pb.js');
			module.exports = proto.helloworld;

4. The tsx filew left unchanged:
		1. import { GreeterClient } from './proto/greeter_grpc_web_pb';

		2. create the grpc client
				const client = new GreeterClient('https://localhost:7001', null, null);

		3. create the request
				const request = new proto.helloworld.HelloRequest();
				
		4. call the grpc method
					client.sayHello(request, {}, (err, response) => {
					      if (err) {
						console.error('Unary Call Error:', err);
					      } else {
						console.log('Unary Call response:', { resp: response.getMessage() });

						setGreeting(response.getMessage());
					      }
					    });

================================================================================================================================
================================================================================================================================
================================================================================================================================
================================================================================================================================
1. open command line in src/prot
2. run the following command: 
			protoc -I=. greeter.proto --js_out=import_style=commonjs:./ --grpc-web_out=import_style=commonjs,mode=grpcwebtext:./
			
			
3. create the file proto/greeter_pb.d.ts


in tsconfig.json
1. add src/proto to include:
	  "include": [
	    "src",
	    "src/proto"
	  ]

In the tsx file:
1. import { GreeterClient } from './proto/greeter_grpc_web_pb';


2. create the grpc client
		const client = new GreeterClient('https://localhost:7001', null, null);

3. create the request
		const request = new proto.helloworld.HelloRequest();
		
4. call the grpc method
			client.sayHello(request, {}, (err, response) => {
			      if (err) {
				console.error('Unary Call Error:', err);
			      } else {
				console.log('Unary Call response:', { resp: response.getMessage() });

				setGreeting(response.getMessage());
			      }
			    });

		





