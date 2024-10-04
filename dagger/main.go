// A generated module for Blink1 functions
//
// This module has been generated via dagger init and serves as a reference to
// basic module structure as you get started with Dagger.
//
// Two functions have been pre-created. You can modify, delete, or add to them,
// as needed. They demonstrate usage of arguments and return types using simple
// echo and grep commands. The functions can be called from the dagger CLI or
// from one of the SDKs.
//
// The first line in this comment block is a short description line and the
// rest is a long description with more detail on the module's purpose or usage,
// if appropriate. All modules should have a short description.

package main

import (
	"context"
	"dagger/blink-1/internal/dagger"
)

type Blink1 struct{}

func (m *Blink1) Build(ctx context.Context, source *dagger.Directory) (string, error) {
	return m.BuildEnv(source).
		WithWorkdir("/src").
		WithExec([]string{"dotnet", "build", "--no-restore"}).
		Stdout(ctx)
}

func (m *Blink1) Test(ctx context.Context, source *dagger.Directory) (string, error) {
	return m.BuildEnv(source).
		WithExec([]string{"dotnet", "test", "--no-build", "--no-restore", "--verbosity", "normal", "Sleddog.Blink1.Tests"}).
		Stdout(ctx)
}

func (m *Blink1) BuildEnv(source *dagger.Directory) *dagger.Container {
	return dag.Container().
		From("mcr.microsoft.com/dotnet/sdk:8.0").
		WithDirectory("/src", source).
		WithWorkdir("/src").
		WithMountedCache("/src/packages", dag.CacheVolume("packages")).
		WithExec([]string{"dotnet", "restore"})
}
