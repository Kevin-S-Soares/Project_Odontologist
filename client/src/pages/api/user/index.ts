import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: { "content-type": "application/json" },
    body: JSON.stringify({
      name: requestBody["name"],
      email: requestBody["email"],
      password: requestBody["password"],
    }),
    method: "POST",
  };
  const body = await fetch(`${process.env.SERVER ?? ""}/api/v1/user`, options);
  return new Response(JSON.stringify(body.ok ? await body.json() : null), {
    status: body.status,
  });
};

export const DELETE: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: {
      "content-type": "application/json",
      authorization: `bearer ${requestBody["token"]}`,
    },
    method: "DELETE",
  };
  const id = requestBody["id"];
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/user/${id}`,
    options,
  );
  return new Response(JSON.stringify(body.ok), {
    status: body.status,
  });
};

export const PUT: APIRoute = async ({ request, redirect }) => {
  const requestBody = await request.json();
  const options = {
    headers: {
      "content-type": "application/json",
      authorization: `bearer ${requestBody["token"]}`,
    },
    body: JSON.stringify({
      id: requestBody["id"],
      name: requestBody["name"],
      role: requestBody["role"],
      profilePictureUrl: "",
    }),
    method: "PUT",
  };
  const body = await fetch(`${process.env.SERVER ?? ""}/api/v1/user/`, options);
  if(body.ok){
    console.log("hi")
    return redirect("/user/refresh_token", 307);
  }
  return new Response("something went wrong while updating user", {
    status: 400
  });
};
